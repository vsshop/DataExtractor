import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { RouteService } from '@route';
import { UploadService } from '@services/upload.services';
import { TableService } from '@services/table.service';
import { DataService } from '@services/data.services';

@Component({
  selector: 'ui-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  @ViewChild('errors', { static: true }) errors!: TemplateRef<HTMLElement>;
  constructor(public service: UploadService,
    private table: TableService,
    private route: RouteService,
    private data: DataService) { }


  clear() {
    this.service.clear();
  }

  next() {
    if (this.service.check) {
      this.table.tables(this.service.data);
      this.table.select(null);
      this.data.load();
      this.route.next("review");
    }
  }
}
