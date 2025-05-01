import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = localStorage.getItem('token');
    const authReq = request.clone({
      setHeaders: { Authorization: `${authToken}` }
    });
    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        
        return throwError(() => error);
      })
    );
  }
}
