import { Component, ElementRef, HostBinding, HostListener, ViewChild, ViewChildren } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RouteService } from '@route';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'ui-modal',
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss',
  animations: [
    trigger('container', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.2s ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ]),
      transition(':leave', [
        animate('0.1s ease-in', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class ModalComponent {
  @ViewChild("container") container!: ElementRef<HTMLElement>;
  @ViewChild(RouterOutlet) outlet!: RouterOutlet;

  @HostListener("click", ["$event"])
  onClick(event: MouseEvent) {
    if (event.target == this.element) this.close();
  }

  constructor(public route: RouteService) { }

  close() {
    this.route.modal(null);
  }

  get element() {
    return this.container.nativeElement;
  }
}
