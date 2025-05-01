import { HttpClient } from '@angular/common/http';
import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';


@Component({
  selector: 'ui-icon',
  template: '',
})
export class IconComponent implements OnInit {
  @Input() url: string = "user.svg";

  @HostBinding('innerHTML') html: SafeHtml | null = null;
  @HostBinding('style.minHeight') height = '0px';

  @HostBinding('class')
  @Input() stylish: string = "first";

  @HostBinding('style.minWidth.px')
  @Input() size: string = "0";

  private static icons: Icon[] = [];

  private base: string = "app/assets/icons/";

  constructor(private http: HttpClient, private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    let save = IconComponent.icons.find(i => i.url == this.url);
    if (save) {
      this.html = save.icon;
      return;
    }


    this.http.get(this.base + this.url, { responseType: 'text' })
      .subscribe(svg => {
        this.html = this.sanitizer.bypassSecurityTrustHtml(svg);
        IconComponent.icons.push({ url: this.url, icon: this.html });
      })
  }
}

type Icon = {
  url: string;
  icon: SafeHtml;
};
