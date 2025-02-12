import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserListingComponent } from './users/user-listing/user-listing.component';
import { AddUserComponent } from './users/add-user/add-user.component';

const routes: Routes = [
  { 
    path: '', 
    component: DashboardComponent,
    children: [
      { path: 'users', component: UserListingComponent },
      { path: 'user', component: AddUserComponent },
      { path: '', redirectTo: 'users', pathMatch: 'full' } // Correct
    ]
  }
];

// const routes: Routes = [
//   // { path: '', redirectTo: '/movies', pathMatch: 'full' },
//   {
//     path: 'users',
//     loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
//   },
//   { path: '', redirectTo: '/users/user', pathMatch: 'full' },
//   { path: 'movies', component: MovieListComponent },
//   { path: 'customers', component: CustomerListComponent },
// ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
