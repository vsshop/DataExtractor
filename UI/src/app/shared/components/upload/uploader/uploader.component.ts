import { Component, ElementRef, EventEmitter, HostListener, Input, Output, ViewChild } from '@angular/core';
import { FileService } from '@services/file.services';
import { CSVFile } from '@models/csv.file';

@Component({
  selector: 'ui-uploader',
  templateUrl: './uploader.component.html',
  styleUrl: './uploader.component.scss'
})
export class UploaderComponent {
  @ViewChild('input', { static: true }) input!: ElementRef<HTMLInputElement>;
  @Output() files = new EventEmitter<File[]>();
  @Input() accept: string = ".csv";
  hover: boolean = false;

  @HostListener("drop", ["$event"])
  onDrop(event: DragEvent) {
    this.hover = false;
    event.preventDefault();
    if (!event.dataTransfer?.files) return;
    let files = Array.from(event.dataTransfer.files);
    this.next(files);
  }

  @HostListener('dragover', ['$event'])
  onDragOver(event: DragEvent) {
    event.preventDefault(); 
    event.dataTransfer!.dropEffect = 'move';
    this.hover = true;
  }

  @HostListener('dragleave', ['$event'])
  onDragLeave(event: DragEvent) {
    this.hover = false;
    event.dataTransfer!.dropEffect = 'none';
  }


  @HostListener("click")
  onClick() {
    this.input.nativeElement.click();
    this.hover = false;
  }

  async onFilesSelected(event: Event) {
    this.hover = false;
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;
    const files: File[] = Array.from(input.files);
    this.next(files);
  }

  private next(files: File[]) {
    const filtered = files.filter(file => this.extension(file));
    this.input.nativeElement.value = '';
    this.files.emit(filtered);
  }

  private extension(file: File) {
    return file.name.toLowerCase().endsWith(this.accept);
  }
}
