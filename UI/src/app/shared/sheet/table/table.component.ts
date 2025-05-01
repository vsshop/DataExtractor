import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ColDef } from 'ag-grid-community';
import { TableService } from '@services/table.service';
import { Table } from '@interfaces/table.interface';

@Component({
  selector: 'ui-table',
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent {
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  table: Table<string[]> | null = null;
  columns: ColDef[] = [];

  constructor(private service: TableService) { }

  ngOnInit() {
    this.service.select$.subscribe(table => {
      this.table = table;

      if (table) {
        this.columns = table.columns.map((col, i) => ({
          headerName: col,
          colId: col,
          editable: true,
          filter: true,
          valueGetter: (params: any) => params.data[i],
          valueSetter: (params: any) => {
            params.data[i] = params.newValue;
            return true;
          }
        }));
      }
    })
  }

  save() {
    const state = this.agGrid.api.getColumnState();
    console.log(state);
  }
}
