import { Component } from '@angular/core';

@Component({
  selector: 'ui-cell-button',
  templateUrl: './cell-button.component.html',
  styleUrl: './cell-button.component.scss'
})
export class CellButtonComponent {
  params: any;

  agInit(params: any): void {
    this.params = params;
  }

  refresh(): boolean {
    return false;
  }

  onEdit() {
    console.log('Редактировать:', this.params.data);
    // this.params.context.componentParent.editRow(this.params.data);
  }

  onDelete() {
    console.log('Удалить:', this.params.data);
    // this.params.context.componentParent.deleteRow(this.params.data);
  }
}
