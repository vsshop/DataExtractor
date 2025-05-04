import { NgModule } from '@angular/core';
import { RouterModule, Routes, provideRouter, withComponentInputBinding, withViewTransitions } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ReviewComponent } from './pages/review/review.component';
import { ModalErrorsComponent } from './shared/components/modal/modal-errors/modal-errors.component';
import { ModalReviewComponent } from './shared/components/modal/modal-review/modal-review.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  { path: "review", component: ReviewComponent },


  { path: "errors", component: ModalErrorsComponent, outlet: 'modal' },
  { path: "review", component: ModalReviewComponent, outlet: 'modal' },
];

@NgModule({
  providers: [provideRouter(routes, withViewTransitions(), withComponentInputBinding())],
  exports: [RouterModule]
})
export class AppRoutingModule { }
