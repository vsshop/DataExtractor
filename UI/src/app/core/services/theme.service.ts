import { Injectable } from '@angular/core';
import { ThemeType } from '../enums/theme.type';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private themeSubject = new BehaviorSubject<ThemeType>(ThemeType.light);
  theme$: Observable<ThemeType> = this.themeSubject.asObservable();

  set(type: ThemeType) {
    this.themeSubject.next(type);
  }
}
