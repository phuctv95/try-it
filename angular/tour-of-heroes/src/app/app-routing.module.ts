import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeroesComponent } from './heroes/heroes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeroDetailComponent } from './hero-detail/hero-detail.component';
import { LifecycleHooksDemoComponent } from './lifecycle-hooks/lifecycle-hooks-demo/lifecycle-hooks-demo.component';
import { ChangeDetectionComponent } from './change-detection/change-detection/change-detection.component';
import { MyFormsComponent } from './my-forms/my-forms/my-forms.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'heroes', component: HeroesComponent },
  { path: 'detail/:id', component: HeroDetailComponent },
  { path: 'lifecycle-hooks', component: LifecycleHooksDemoComponent },
  { path: 'change-detection', component: ChangeDetectionComponent },
  { path: 'my-forms', component: MyFormsComponent },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
