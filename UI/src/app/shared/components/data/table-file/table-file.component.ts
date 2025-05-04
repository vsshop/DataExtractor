import { Component, HostListener, Input } from '@angular/core';
import { TableFile } from '@models/table.file';
import { TableService } from '@services/table.service';

@Component({
  selector: 'ui-table-file',
  templateUrl: './table-file.component.html',
  styleUrl: './table-file.component.scss'
})
export class TableFileComponent {
  @Input() table: TableFile | null = null;

  @HostListener("click")
  onSelect() {
    this.service.select(this.table);
  }
  constructor(public service: TableService) { }
}
