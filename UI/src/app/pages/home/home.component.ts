import { Component, OnInit } from '@angular/core';
import { Browser } from '@browser';
import { RouteService } from '@route';
import { FileService } from '@services/file.services';

@Component({
  selector: 'ui-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private browser: Browser, private fileService: FileService, private route: RouteService) { }


  clear() {
    this.fileService.load(null);
  }

  next() {
    this.route.next("review");
  }
}
