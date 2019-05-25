import { Folder } from './../models/folder';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Params, ActivatedRoute } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class FolderService {
  constructor(private http: HttpClient) {}
  readonly baseUrl = 'http://localhost:52081/api/folders';
  getRootFolder(): Observable<Folder> {
    const myHeaders = this.getHeader();
    return this.http.get<Folder>(this.baseUrl, {headers: myHeaders});
  }
  getFolderById(id: number): Observable<Folder> {
    console.log('get');
    const myHeaders = this.getHeader();
    return this.http.get<Folder>(this.baseUrl + '/' + id, {headers: myHeaders});
   }
   getHeader() {
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.set('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
    return myHeaders;
   }
  createFolder(folder: Folder) {
    console.log('create folder');

    const data = new FormData();
    data.append('parentId', folder.ParentFolderId.toString());
    data.append('name', folder.Name);

    const myHeaders = this.getHeader();
    return this.http.post(this.baseUrl, data, { headers: myHeaders });
  }
  deleteFolderById(folderId: number) {
    const myHeaders = this.getHeader();

    return this.http.delete(this.baseUrl + '/' + folderId, {headers: myHeaders});
  }
  updateFolder(newFolder: Folder) {
    let myHeaders = this.getHeader();
    myHeaders = myHeaders.set('Content-Type', 'application/json');

    const json = JSON.stringify(newFolder);

    return this.http.put(this.baseUrl + '/' + newFolder.Id, json, {headers: myHeaders});
  }
}
