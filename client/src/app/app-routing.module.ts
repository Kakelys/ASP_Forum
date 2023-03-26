import { AuthComponent } from './auth/auth.component';
import { SectionListComponent } from './section-list/section-list.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'main', component: SectionListComponent},
  {path: 'auth', component: AuthComponent},
  {path: '', redirectTo: 'main', pathMatch: 'full'},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
