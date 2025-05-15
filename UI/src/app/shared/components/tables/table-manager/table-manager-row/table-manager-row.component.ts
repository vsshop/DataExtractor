import { Component, Input, OnInit } from '@angular/core';
import { TableFile } from '../../../../models/table.file';
import { TableService } from '../../../../../services/table.service';

@Component({
  selector: 'ui-table-manager-row',
  templateUrl: './table-manager-row.component.html',
  styleUrl: './table-manager-row.component.scss'
})
export class TableManagerRowComponent implements OnInit {
  @Input() table: TableFile | null = null;
  @Input() rename: boolean = false;
  @Input() index: number = 0;

  constructor(private service: TableService) { }

  ngOnInit(): void {
    let original = this.table?.columnsOriginal[this.index];
    let modify = this.table?.columnsModified[this.index];
    this.rename = original != modify;
  }

  onEdit() {
    this.service.edit(this.index);
  }

  onRename() {
    let original = this.table?.columnsOriginal[this.index];
    let modify = this.table?.columnsModified[this.index];
    this.rename = original != modify;
  }
}
