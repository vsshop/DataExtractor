import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { XMLTable } from '@interfaces/xmltable.interface';

@Component({
  selector: 'ui-xmlreader',
  templateUrl: './xmlreader.component.html',
  styleUrl: './xmlreader.component.scss'
})
export class XMLReaderComponent {
  @ViewChild('input', { static: true }) input!: ElementRef<HTMLInputElement>;
  tables: XMLTable[] = [];

  click() {
    this.input.nativeElement.click();
  }

  async onFilesSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const files: File[] = Array.from(input.files);
    this.tables = await this.parseXmlTables(files[0]);
  }

  async parseXmlTables(file: File) {
    const xml = await file.text();
    const doc = new DOMParser().parseFromString(xml, 'application/xml');
    const tables = Array.from(doc.getElementsByTagName('Table'));
    return tables.map(table => new XMLTable(table))
  }
}
