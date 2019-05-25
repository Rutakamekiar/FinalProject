import { UpdateFileComponent } from './components/update-file/update-file.component';
import { CreateFolderComponent } from './components/create-folder/create-folder.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginUserComponent } from './components/login-user/login-user.component';
import { HttpClientModule } from '@angular/common/http';
import { GetFolderContentComponent } from './components/get-folder-content/get-folder-content.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';
import { GlobalErrorHandler } from './error-handler';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { DeleteFolderComponent } from './components/delete-folder/delete-folder.component';
import { UpdateFolderComponent } from './components/update-folder/update-folder.component';
import { DeleteFileComponent } from './components/delete-file/delete-file.component';
import { DownloadFileComponent } from './components/download-file/download-file.component';
import { LogoutUserComponent } from './components/logout-user/logout-user.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginUserComponent,
    LogoutUserComponent,
    GetFolderContentComponent,
    RegisterUserComponent,
    CreateFolderComponent,
    UploadFileComponent,
    DeleteFolderComponent,
    UpdateFolderComponent,
    DeleteFileComponent,
    UpdateFileComponent,
    DownloadFileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
    provide: ErrorHandler,
    useClass: GlobalErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
