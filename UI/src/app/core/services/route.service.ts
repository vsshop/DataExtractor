import { Injectable, TemplateRef } from "@angular/core";
import { Location } from '@angular/common';
import { ActivatedRouteSnapshot, NavigationEnd, Params, Router, RouterState } from "@angular/router";
import { BehaviorSubject, Observable, Subject, filter } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RouteService {
  private uri = "";
  private history: string[] = ["/"];

  private navigation = new Subject<RouterState>();
  navigate$: Observable<RouterState> = this.navigation.asObservable();

  private params = new BehaviorSubject<Params>({});
  params$: Observable<Params> = this.params.asObservable();

  private ref = new BehaviorSubject<TemplateRef<HTMLElement> | null>(null);
  ref$: Observable<TemplateRef<HTMLElement> | null> = this.ref.asObservable();

  constructor(private router: Router, private location: Location) {
    this.routeEvent(router.events).subscribe(() => this.story(router));
  }
  
  private routeEvent(events: Observable<any>) {
    return events.pipe(filter((event) => event instanceof NavigationEnd))
  }
  
  next(url: string, params: Params | null = null) {
    this.modal(null);
    this.router.navigate([this.uri + "/" + url], { queryParams: params });
  }

  before() {
    let current = this.history.pop() ?? "";
    let last = this.history.pop() ?? "";
    this.location.back();

    return current !== "";
  }

  modal(modal: TemplateRef<HTMLElement> | null) {
    this.ref.next(modal);
  }
  
  private story(router: Router) {
    const root = router.routerState.snapshot.root;
    this.params.next(root.queryParams);
    this.navigation.next(router.routerState);

    if (router.url === `/${this.uri}`) {
      this.history = [];
      return;
    }

    this.history.push(router.url);
    this.navigation.next(router.routerState);
  }
}
