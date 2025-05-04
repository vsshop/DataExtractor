import { Component, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';

class Click {
  x: number = 0;
  y: number = 0;

  constructor(x: number, y: number) {
    this.x = x;
    this.y = y;
  }
}


@Component({
  selector: 'ui-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  @Input() font: string = "fs18";
  @Input() offset: string = "pd16";
  @Input() radius: string = "br12";
  @Input() stylish: string = "first";

  @Output() select = new EventEmitter();

  clicks: Click[] = [];

  @HostListener("mousedown", ["$event"])
  onClick(e: MouseEvent) {
    let rect = this.rect;
    let x = e.clientX - rect.x;
    let y = e.clientY - rect.y;
    this.clicks.push(new Click(x, y))
  }

  constructor(private component: ElementRef<HTMLElement>) { }

  end(index: number) {
    this.clicks.splice(index, 1);
    this.select.emit();
  }

  get element() {
    return this.component.nativeElement;
  }

  get rect() {
    return this.element.getBoundingClientRect();
  }
}
