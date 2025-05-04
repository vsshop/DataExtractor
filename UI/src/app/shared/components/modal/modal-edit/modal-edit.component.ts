import { Component } from '@angular/core';
import { take } from 'rxjs/operators';
import { TableService } from '@services/table.service';
import { Table } from '@interfaces/table.interface';
import { TableFile } from '../../../models/table.file';
import { RouteService } from '../../../../core/services/route.service';

@Component({
  selector: 'ui-modal-edit',
  templateUrl: './modal-edit.component.html',
  styleUrl: './modal-edit.component.scss'
})
export class ModalEditComponent {
  table: Table<string[]> | null = null;
  constructor(private service: TableService, private route: RouteService) { }

  ngOnInit() {
    const table = this.service.table;
    const column = this.service.column;
    if (!table) return;

    this.table = {
      name: table.columnsModified[column],
      columns: this.columns(table, column),
      rows: this.rows(table, column)
    }
  }

  save() {
    const table = this.service.table;
    const columnIndex = this.service.column;
    if (!table || !this.table) return;

    table.modifiers[columnIndex] = false;
    this.table.rows.forEach((row, rowIndex) => {
      let value = row[1];
      table.dataModified[columnIndex][rowIndex] = value;

      let old = table.dataOriginal[columnIndex][rowIndex];
      if (value !== old) table.modifiers[columnIndex] = true;
    });
    this.route.modal(null);
  }

  private rows(table: TableFile, index: number) {
    const original = table.dataOriginal[index] ?? [];
    const modified = table.dataModified[index] ?? [];
    return Array.from({ length: original.length }, (_, row) => [original[row], modified[row] ]);
  }

  private columns(table: TableFile, index: number) {
    return [table.columnsOriginal[index], table.columnsModified[index]];
  }
}
