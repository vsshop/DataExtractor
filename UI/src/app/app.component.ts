import { Component, HostListener, OnInit } from '@angular/core';
import { ThemeService } from './core/services/theme.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  template: `<ui-modal/><router-outlet class='max'></router-outlet>`
})
export class AppComponent implements OnInit {
  @HostListener("document:mousedown", ["$event"])
  onClick(e: MouseEvent) {
    this.root(e.clientX, e.clientY);
  }

  @HostListener('document:contextmenu', ['$event'])
  onContext(e: MouseEvent) {
    e.preventDefault();
  }
  
  @HostListener('document:dragover', ['$event'])
  onDragOver(event: DragEvent) {
    event.preventDefault();
    const target = event.target as HTMLElement;
    if (!target.classList.contains("uploader")) {
      event.dataTransfer!.dropEffect = 'none';
    }
  }

  @HostListener('document:drop', ['$event'])
  onDrop(event: DragEvent) {
    event.preventDefault();
  }

  constructor(private theme: ThemeService, private translate: TranslateService) {
    translate.addLangs(['en']);
    translate.setDefaultLang('en');
    translate.use('en');
  }

  ngOnInit(): void {
    this.theme.theme$.subscribe(t => document.body.className = t);
  }

  root(x: number, y: number) {
    const root = document.documentElement;
    root.style.setProperty('--x', `${x}px`);
    root.style.setProperty('--y', `${y}px`);
  }
}
