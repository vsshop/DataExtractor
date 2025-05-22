import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { TableService } from '@services/table.service';
import { ColDef, GridOptions } from 'ag-grid-community';
import { Table } from '@interfaces/table.interface';

@Component({
  selector: 'ui-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent implements OnChanges, AfterViewInit {
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  @Input() table: Table<string[]> | null = null;
  @Input() highlight: boolean[][] = [];
  @Input() editable: boolean[] = [];
  @Input() hide: boolean = false;

  rows: string[][] = [];
  columns: ColDef[] = [];
  gridOptions: GridOptions = {
    enableCellTextSelection: true,
    copyHeadersToClipboard: true,
    defaultColDef: {  editable: true },
    suppressClipboardPaste: false
  };
  constructor(private service: TableService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes["table"]) return;
    if (!this.table) return;

    this.rows = this.table.rows;
    this.columns = this.table.columns
      .map((col, i) => ({
      headerName: col, colId: col,
      editable: this.editable[i] ?? false,
      filter: true, flex: 1,
      hide: this.hide && col == '',
      minWidth: Math.max(120, col.length * 10),
      cellClass: (params: any) => {
        const rowIndex = params.node.rowIndex;
        const isHighlighted = this.highlight?.[rowIndex]?.[i];
        return isHighlighted ? 'highlight' : '';
      },
      valueGetter: (params: any) => params.data[i],
      valueSetter: (params: any) => {
        params.data[i] = params.newValue;
        return true;
      }
    }));
  }

  ngAfterViewInit(): void {
    this.agGrid.api.addEventListener('pasteStart', this.onPasteStart.bind(this));
  }

  onPasteStart(event: any) {
    const pastedData: string[][] = event.data;
    const startRow = event.api.getFocusedCell()?.rowIndex ?? 0;
    const column = event.api.getFocusedCell()?.column;

    if (!column?.isCellEditable(startRow)) return;

    for (let i = 0; i < pastedData.length; i++) {
      const rowNode = event.api.getDisplayedRowAtIndex(startRow + i);
      if (!rowNode) {
        this.rows.push([]);
      }
      this.rows[startRow + i] ??= [];
      this.rows[startRow + i][column.getColId()] = pastedData[i][0]; 
    }

    event.api.setRowData(this.rows);
  }
}
