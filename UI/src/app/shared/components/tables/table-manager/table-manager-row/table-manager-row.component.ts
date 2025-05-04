import { Component, Input } from '@angular/core';
import { TableFile } from '../../../../models/table.file';
import { TableService } from '../../../../../services/table.service';

@Component({
  selector: 'ui-table-manager-row',
  templateUrl: './table-manager-row.component.html',
  styleUrl: './table-manager-row.component.scss'
})
export class TableManagerRowComponent {
  @Input() table: TableFile | null = null;
  @Input() index: number = 0;

  constructor(private service: TableService) { }

  onEdit() {
    this.service.edit(this.index);
  }
}
