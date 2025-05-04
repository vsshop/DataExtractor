import { Component } from '@angular/core';
import { UploadService } from '@services/upload.services';
import { trigger, transition, style, animate } from '@angular/animations';
import { CSVFile } from '@models/csv.file';

@Component({
  selector: 'ui-upload',
  templateUrl: './upload.component.html',
  styleUrl: './upload.component.scss',
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
export class UploadComponent {
  constructor(public service: UploadService) { }

  onFiles(files: File[]) {
    this.service.openCSV(files);
  }

  onRemove(csv: CSVFile) {
    this.service.removeCSV(csv);
  }
}
