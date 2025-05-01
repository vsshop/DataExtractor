import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

interface AngularResponseMessage {
  method: string;
  result: any;
}

@Injectable({
  providedIn: 'root'
})
export class Browser {
  private handlers = new Map<string, Subject<any>>();
  constructor() {
    this.document?.addEventListener('message', (e: any) => {
      const data: AngularResponseMessage = e.data;
      if (data?.method) {
        const subject = this.handlers.get(data.method);
        if (subject) subject.next(data.result);
      }
    });
  }

  invoke(method: string, ...args: any[]) {
    this.document?.postMessage({
      type: 'invoke', method,
      payload: JSON.stringify(args)
    });
  }

  subscribe<T>(method: string, action: (args: T) => void) {
    let subject = this.handlers.get(method);
    if (!subject) {
      subject = new Subject<T>();
      this.handlers.set(method, subject);
    }
    subject.subscribe(action);
  }

  private get document() {
    let browser = window as any;
    return browser.chrome?.webview;
  }
}
