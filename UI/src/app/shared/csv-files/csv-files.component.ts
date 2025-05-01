import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { FileService } from '@services/file.services';
import { Core } from '../../core/services/core.service';
import { trigger, transition, style, animate, query, stagger } from '@angular/animations';

@Component({
  selector: 'ui-csv-files',
  templateUrl: './csv-files.component.html',
  styleUrl: './csv-files.component.scss',
  animations: [
    trigger('file', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-20px)' }),
        animate('0.3s ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ]),
      transition(':leave', [
        animate('0.3s ease-in', style({
          opacity: 0, marginTop: '-32px',
          transformOrigin: 'right',
          transform: 'scale(0.6)'
        }))
      ])
    ])
  ]
})
export class CsvFilesComponent implements OnInit {
  @Input() expand: string | null = null;
  files: File[] = [];
  constructor(private fileService: FileService, private core: Core) { }

  ngOnInit(): void {
    this.fileService.load$.subscribe(files => this.files = files ?? [])
  }

  delete(file: File) {
    this.fileService.remove(file);
  }

  select(file: File) {
    this.fileService.select(file);
  }
}
