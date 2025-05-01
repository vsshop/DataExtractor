import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'ui-reader-edit',
  templateUrl: './reader-edit.component.html',
  styleUrl: './reader-edit.component.scss'
})
export class ReaderEditComponent {

  @HostListener("click")
  onClick() {
    console.log(123);
  }

  agInit(params: any): void {
    
  }
}
