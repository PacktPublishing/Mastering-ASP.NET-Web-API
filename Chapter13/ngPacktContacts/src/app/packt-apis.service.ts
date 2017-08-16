import { Contacts } from './contacts.class';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/catch';

@Injectable()
export class PacktAPIsService {

  private actionUrl: string;
  public token: string;
  constructor(private _http: Http) {
        this.actionUrl = 'http://localhost:50461/api/contacts/';
        let curntUser = JSON.parse(localStorage.getItem('currentUser'));
        if (curntUser !== null) {
          this.token = curntUser.token;
        }
  }
public GetAll = (): Observable<any> => {
    let headers = new Headers({'Content-Type': 'application/json', 'packt-book' : 'Mastering Web API',
   'Authorization': 'Bearer ' + this.token});
  let options = new RequestOptions({ headers: headers });
    return this._http.get(this.actionUrl, options)
        .map((response: Response) => <any>response.json());
}

public addContacts(ContactsObj: Contacts){
  let headers = new Headers({'Content-Type': 'application/json', 'packt-book' : 'Mastering Web API',
   'Authorization': 'Bearer ' + this.token});

  let options = new RequestOptions({ headers: headers });
    return this._http.post(this.actionUrl, JSON.stringify(ContactsObj), options)
      .map((response: Response) => <any>response.json());
}

public UpdateContacts(ContactsObj: Contacts, contactId:number){
  let headers = new Headers({'Content-Type': 'application/json', 'packt-book' : 'Mastering Web API',
   'Authorization': 'Bearer ' + this.token});
  let options = new RequestOptions({ headers: headers });
    return this._http.put((this.actionUrl + contactId), JSON.stringify(ContactsObj), options)
      .map((response: Response) => <any>response.json());
}

public DeleteContacts(contactId:number){
  let headers = new Headers({'Content-Type': 'application/json', 'packt-book' : 'Mastering Web API',
   'Authorization': 'Bearer ' + this.token});
  let options = new RequestOptions({ headers: headers });
    return this._http.delete((this.actionUrl + contactId), options)
      .map((response: Response) => <any>response.json());
}

public login(username: string, password: string): Observable<boolean> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
        let Authurl = 'http://localhost:50461/api/auth/token';
        return this._http.post(Authurl,
        JSON.stringify({ username: username, password: password }), options)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                console.log(response);
                let token = response.json() && response.json().token;
                if (token) {
                    // set token & UserId property
                    this.token = token;
                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            })
        .catch(this.handleError);
    }

    handleError(error: any) {
        return Observable.throw('Server error ' + error);
    }

}
