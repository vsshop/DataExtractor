import { Component } from '@angular/core';
import { RouteService } from '../../core/services/route.service';

@Component({
  selector: 'ui-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.scss'
})
export class ReviewComponent {

  constructor(private route: RouteService) { }

  back() {
    this.route.next("");
  }
}
