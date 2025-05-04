import { Component, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { TableService } from '@services/table.service';
import { ColDef } from 'ag-grid-community';
import { Table } from '@interfaces/table.interface';

@Component({
  selector: 'ui-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent implements OnChanges {
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  @Input() table: Table<string[]> | null = null;
  @Input() highlight: boolean[][] = [];
  @Input() editable: boolean[] = [];

  rows: string[][] = [];
  columns: ColDef[] = [];
  constructor(private service: TableService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (!changes["table"]) return;
    if (!this.table) return;

    this.rows = this.table.rows;
    this.columns = this.table.columns.map((col, i) => ({
      headerName: col, colId: col,
      editable: this.editable[i] ?? false,
      filter: true, flex: 1,
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

  save() {
    const state = this.agGrid.api.getColumnState();
  }
}
