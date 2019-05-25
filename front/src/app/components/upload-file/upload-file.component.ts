import { FileService } from './../../services/file.service';
import { Component, Inject } from '@angular/core';
import { GetFolderContentComponent } from '../get-folder-content/get-folder-content.component';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent {
  fileToUpload: File = null; 
  creating = false;
  accessLevel = '1';
  constructor(private service: FileService,
              @Inject(GetFolderContentComponent) private parent: GetFolderContentComponent) { }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }
  sendFile() {
    this.service.uploadFile(this.fileToUpload, this.parent.myFolder.Id, this.accessLevel).subscribe(() =>
      this.parent.openFolder(this.parent.myFolder.Id)
    );
    this.creating = false;
  }
}
