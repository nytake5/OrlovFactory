import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ApiClientService } from 'src/app/service/api-client.service';
import { AuthService } from 'src/app/service/auth.service';
import { Employee } from 'src/app/types/Employee';
import { UserCredentials } from 'src/app/types/UserCredentials';

@Component({
  selector: 'app-body',
  templateUrl: './body.component.html',
  styleUrls: ['./body.component.scss']
})
export class BodyComponent implements OnInit {

  @ViewChild('messageContainer', {
    read: ViewContainerRef  
  })
  alertContainer!: ViewContainerRef;

  public userData$!: Observable<UserCredentials>;
  public isAuth$: Observable<boolean> = of(false);
  public ListOfUser$: Observable<Employee[]> = of([]);
  dataSource = <Employee[]>[];
  displayedColumns: string[] = ['passNumber', 'firstName', 'lastName', 'patronymic', 'post'];

  constructor(public authService: AuthService,
              public clientService: ApiClientService) {
    console.log('Starting BodyComponent');
  }
  
  ngOnInit(): void {
    this.isAuth$ = this.authService.checkCredsInCookie();
    this.isAuth$.subscribe(x => {
      if (x == true)
      {
        this.alertContainer.clear();
      }
    })
    this.ListOfUser$ = this.clientService.getAllEmployees();
    this.ListOfUser$.subscribe(x => 
      { 
        console.log(x)  
        this.dataSource = x 
      });
  }
}
