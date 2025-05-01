import { NgModule } from '@angular/core';
import { RouterModule, Routes, provideRouter, withComponentInputBinding, withViewTransitions } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ReviewComponent } from './pages/review/review.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "review", component: ReviewComponent },
];

@NgModule({
  providers: [provideRouter(routes, withViewTransitions(), withComponentInputBinding())],
  exports: [RouterModule]
})
export class AppRoutingModule { }
