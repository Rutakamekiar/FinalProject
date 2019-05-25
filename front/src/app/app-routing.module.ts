import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { GetFolderContentComponent } from './components/get-folder-content/get-folder-content.component';
import { BrowserModule } from '@angular/platform-browser';
import {RegisterUserComponent} from './components/register-user/register-user.component';

const routes: Routes = [
  { path: '', component: RegisterUserComponent},
  { path: 'login', component: LoginUserComponent},
  { path: 'folders/:id', component: GetFolderContentComponent},
  { path: 'folders', component: GetFolderContentComponent}
  // { path: 'file/:id', component: GetFolderContentComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes), BrowserModule],
  declarations: [],
  exports: [RouterModule]
})
export class AppRoutingModule { }
