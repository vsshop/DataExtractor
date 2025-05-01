import { Component, HostBinding, Input } from '@angular/core';

@Component({
  selector: 'ui-panel',
  templateUrl: './panel.component.html',
  styleUrl: './panel.component.scss'
})
export class PanelComponent {
  @Input() offset: string = "pd16";

  @HostBinding('style.minWidth.px')
  @Input() size: string = "0";
}
