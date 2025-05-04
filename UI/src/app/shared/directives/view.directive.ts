import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { Core } from '@core';

@Directive({
  selector: '[view]'
})
export class ViewDirective implements OnInit {
  @Input() view: string | null = null;
  @Input() group: string | null = null;
  constructor(private component: ElementRef<HTMLElement>) { }
  
  ngOnInit(): void {
    const name = this.view ? this.view : Core.guid();
    this.style.setProperty('view-transition-name', "view-" + name);

    if (this.group) {
      this.style.setProperty('view-transition-class', this.group);
    }
  }
  
  get element() {
    return this.component.nativeElement;
  }

  get style() {
    return this.element.style;
  }
}
