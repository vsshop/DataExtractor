import { trigger, transition, style, animate } from '@angular/animations';
import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { RouteService } from '../../core/services/route.service';
import { TableService } from '@services/table.service';
import { TableFile } from '../../shared/models/table.file';

@Component({
  selector: 'ui-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.scss',
  animations: [
    trigger('manager', [
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
export class ReviewComponent implements OnInit {
  @ViewChild('review', { static: true }) review!: TemplateRef<HTMLElement>;
  @ViewChild('edit', { static: true }) edit!: TemplateRef<HTMLElement>;
  constructor(public service: TableService, private route: RouteService) { }

  ngOnInit(): void {
    this.service.edit$.pipe().subscribe(column => {
      if (column != -1) this.route.modal(this.edit)
    })
  }

  back() {
    this.route.before();
  }

  onReview() {
    this.route.modal(this.review);
  }

  onSave() {
    const table = this.service.table;
    if (table) table.save();
  }
}
