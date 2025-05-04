import { Component, HostListener, Input } from '@angular/core';
import { XMLFile } from '@models/xml.file';
import { FileService } from '@services/file.services';

@Component({
  selector: 'ui-xml-file',
  templateUrl: './xml-file.component.html',
  styleUrl: './xml-file.component.scss'
})
export class XMLFileComponent {
  @Input() xml: XMLFile | null = null;

  constructor(public service: FileService) { }

  @HostListener('mouseenter')
  onMouseEnter() {
    this.service.hover(this.xml);
  }

  @HostListener('mouseleave')
  onMouseLeave() {
    this.service.hover(null);
  }

  @HostListener("click")
  onSelect() {
    //this.fileService.select(this.file);
  }
}
