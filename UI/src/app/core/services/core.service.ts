import { Injectable, NgZone } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { BaseResponse } from '../models/base.response';

@Injectable({
  providedIn: 'root'
})
export class Core {
  constructor(private http: HttpClient, private zone: NgZone) {}

  async get<T>(url: string) {
    const request = this.http.get<BaseResponse<T>>(url);
    return await firstValueFrom(request);
  }

  async post<T>(url: string, body: any) {
    const request = this.http.post<BaseResponse<T>>(url, body);
    return await firstValueFrom(request);
  }

  delay(ms: number): Promise<void> {
    return Core.delay(ms);
  }

  static delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  static guid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
      const r = (Math.random() * 16) | 0;
      const v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  animate(fn: () => void): void {
    if ('startViewTransition' in document) {
      (document as any).startViewTransition(() => {
        return new Promise<void>((resolve) => {
          this.zone.run(() => {
            fn();
            resolve();
          });
        });
      });
    }
    else {
      this.zone.run(fn);
    }
  }

  static async read(file: File, encoding: string = 'utf-8') {
    return await new Promise<string>((resolve, reject) => {
      const reader = new FileReader();

      reader.onload = () => resolve(reader.result as string);
      reader.onerror = () => reject(reader.error);
      reader.readAsText(file, encoding);
    });
  }
}
