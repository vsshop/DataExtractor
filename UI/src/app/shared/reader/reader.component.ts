import { Component, OnDestroy, OnInit } from '@angular/core';
import { FileService } from '@services/file.services';
import { ColDef } from 'ag-grid-community';
import { Subscription } from 'rxjs';
import { ReaderCheckComponent } from './reader-check/reader-check.component';
import { ReaderEditComponent } from './reader-edit/reader-edit.component';
import { ReaderInputComponent } from './reader-input/reader-input.component';

@Component({
  selector: 'ui-reader',
  templateUrl: './reader.component.html',
  styleUrl: './reader.component.scss'
})
export class ReaderComponent implements OnInit, OnDestroy {
  private subscription!: Subscription;

  csvColumns = [
    { name: 'Name', alias: 'Name', selected: true },
    { name: 'Email', alias: 'Email', selected: true },
    { name: 'Phone', alias: 'Phone', selected: false },
    { name: 'Address', alias: 'Address', selected: false }
  ];

  columnDefs: ColDef[] = [
    { headerName: 'CSV Column', field: 'name', filter: true, flex: 1 },
    {
      headerName: 'Rename', field: 'alias',
      cellRenderer: ReaderInputComponent, flex: 1
    },
    {
      headerName: 'Change', field: 'selected',
      cellRenderer: ReaderCheckComponent, width: 80
    },
    {
      headerName: 'Edit', field: 'edit',
      cellRenderer: ReaderEditComponent, width: 60
    }
  ];


  constructor(private fileService: FileService) { }
   

  ngOnInit(): void {
    this.subscription = this.fileService.select$.subscribe(async file => {
      if (file) {
        let csv = await this.readCSV(file);
        console.log(csv);
      }
    })
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  async readCSV(file: File, splitter: string = ';'): Promise<{ headers: string[], rows: string[][] }> {
    const text = await file.text();
    const lines = text.split(/\r?\n/).filter(line => line.trim().length > 0);
    const headers = lines[0].split(splitter).map(h => h.trim());
    const rows = lines.slice(1)
        .map(line => line
        .split(splitter)
        .map(v => v.trim()));

    return { headers, rows };
  }
}
