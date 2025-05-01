import { Component } from '@angular/core';
import { ICellRendererAngularComp } from 'ag-grid-angular';

@Component({
  selector: 'ui-reader-check',
  templateUrl: './reader-check.component.html',
  styleUrl: './reader-check.component.scss'
})
export class ReaderCheckComponent implements ICellRendererAngularComp  {
  value: boolean = false;

  agInit(params: any): void {
    this.value = params.value;
  }

  refresh(): boolean {
    return false;
  }
}
