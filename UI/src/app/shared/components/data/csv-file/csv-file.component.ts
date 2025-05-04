import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { CSVFile } from '@models/csv.file';
import { FileService } from '@services/file.services';
import { RouteService } from '../../../../core/services/route.service';

@Component({
  selector: 'ui-csv-file',
  templateUrl: './csv-file.component.html',
  styleUrl: './csv-file.component.scss',
})
export class CsvFileComponent {
  @Input() csv: CSVFile | null = null;

  constructor(public service: FileService, private component: ElementRef<HTMLElement>, private route: RouteService) { }

  @HostListener('mouseenter')
  onMouseEnter() {
    this.service.hover(this.csv);
  }

  @HostListener('mouseleave')
  onMouseLeave() {
    this.service.hover(null);
  }

  @HostListener("click", ["$event"])
  onSelect(event: MouseEvent) {
    if (event.target == this.element) {
      
    }
  }

  get element() {
    return this.component.nativeElement;
  }
}
