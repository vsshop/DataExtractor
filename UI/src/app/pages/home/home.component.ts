import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { RouteService } from '@route';
import { UploadService } from '@services/upload.services';
import { TableService } from '../../services/table.service';

@Component({
  selector: 'ui-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  @ViewChild('errors', { static: true }) errors!: TemplateRef<HTMLElement>;
  constructor(public service: UploadService, private table: TableService, private route: RouteService) { }


  clear() {
    this.service.clear();
  }

  next() {
    if (this.service.check) {
      this.table.select(null);
      this.table.tables(this.service.data);
      this.route.next("review");
    }
  }
}
