import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { FileService } from '../../../services/file.services';

@Component({
  selector: 'ui-csv-file',
  templateUrl: './csv-file.component.html',
  styleUrl: './csv-file.component.scss',
})
export class CsvFileComponent {
  @Input() file: File | null = null;
  @Input() active: boolean = false;

  constructor(private fileService: FileService) {}

  @HostListener("click")
  onSelect() {
    this.fileService.select(this.file);
  }

  onDelete() {
    this.fileService.remove(this.file!);
  }
}
