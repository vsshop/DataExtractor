import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { FileService } from '@services/file.services';

@Component({
  selector: 'ui-uploader',
  templateUrl: './uploader.component.html',
  styleUrl: './uploader.component.scss'
})
export class UploaderComponent {
  @ViewChild('input', { static: true }) input!: ElementRef<HTMLInputElement>;
  hover: boolean = false;
  constructor(private fileService: FileService) { }

  @HostListener("drop", ["$event"])
  onDrop(event: DragEvent) {
    event.preventDefault();
    if (!event.dataTransfer?.files) return;
    let files = Array.from(event.dataTransfer.files);
    this.fileService.load(files);
    this.hover = false;
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

  onFilesSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;
    const files: File[] = Array.from(input.files);
    this.fileService.load(files);
    this.hover = false;
  }
}
