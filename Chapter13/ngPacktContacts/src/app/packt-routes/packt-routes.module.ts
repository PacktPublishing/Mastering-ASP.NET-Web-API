import { ContactsComponent } from './../contacts/contacts.component';
import { LoginComponent } from './../login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        component: LoginComponent,
    },
    {
        path: 'contacts',
        component: ContactsComponent,
    }
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
        RouterModule
    ],
  declarations: []
})

export class PacktRoutesModule { }
