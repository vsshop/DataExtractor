import { Component, Input } from '@angular/core';
import { TableFile } from '@models/table.file';

@Component({
  selector: 'ui-table-manager',
  templateUrl: './table-manager.component.html',
  styleUrl: './table-manager.component.scss'
})
export class TableManagerComponent {
  @Input() table: TableFile | null = null;
}
