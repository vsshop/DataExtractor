import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class LocalizeService {
  constructor(@Inject(DOCUMENT) private document: Document) { }

  changeLocale(locale: string) {
    this.document.location.reload();
    localStorage.setItem('locale', locale);
  }

  getLocale(): string {
    return localStorage.getItem('locale') || 'uk-Ua';
  }
}
