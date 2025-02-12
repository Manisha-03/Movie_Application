import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userAddUrl = 'https://localhost:7266/api/User'; 

  constructor(private http: HttpClient) {}

  saveUser(userData: any): Observable<any> {
    return this.http.post('https://localhost:7266/api/User/UserAdd', userData, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }
  
  getCountries(): Observable<any> {
    return this.http.get(`${this.userAddUrl}/Country`);
  }

  getUsers(): Observable<any> {
    return this.http.get(`${this.userAddUrl}/GetUsers`);
  }

  updateUserStatus(customerId: number, active: boolean): Observable<any> {
    return this.http.patch(`${this.userAddUrl}/UpdateUserStatus`, { customerId, active });
  }

}
