import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { XMLTable } from '@interfaces/xmltable.interface';
import { UploadService } from '@services/upload.services';

@Component({
  selector: 'ui-xmlreader',
  templateUrl: './xmlreader.component.html',
  styleUrl: './xmlreader.component.scss'
})
export class XMLReaderComponent {
  @ViewChild('input', { static: true }) input!: ElementRef<HTMLInputElement>;

  constructor(public service: UploadService) { }

  click() {
    this.input.nativeElement.click();
  }

  onFile(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;
    this.service.openXML(input.files[0]);
    this.input.nativeElement.value = '';
  }
}
