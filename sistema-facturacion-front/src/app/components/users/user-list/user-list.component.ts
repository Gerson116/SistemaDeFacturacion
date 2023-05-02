import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { lastValueFrom } from 'rxjs';
import { UsersService } from 'src/app/services/users/users.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  dataSource: any;

  constructor(){
  }

  ngOnInit(){
  }
}
