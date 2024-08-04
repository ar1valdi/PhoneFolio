import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewContactFormComponent } from './forms/new-contact-form/new-contact-form.component';
import { ContentHostComponent } from './content-host/content-host.component';
import { ContactDetailsComponent } from './contact-details/contact-details.component';
import { EditContactFormComponent } from './forms/edit-contact-form/edit-contact-form.component';
import { RegisterFormComponent } from './forms/register-form/register-form.component';
import { LoginFormComponent } from './forms/login-form/login-form.component';
import { ErrorComponent } from './error/error.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'PhoneFolio'
    },
    {
        path: 'contacts',
        component: HomeComponent,
        title: 'Contact manager'
    },
    {
        path: 'contacts/new',
        component: ContentHostComponent,
        data: {component: NewContactFormComponent},
        title: 'New contact'
    },
    {
        path: 'contact/:id',
        component: ContentHostComponent,
        data: {component: ContactDetailsComponent},
        title: 'New contact'
    },
    {
        path: 'contact/edit/:id',
        component: ContentHostComponent,
        data: {component: EditContactFormComponent},
        title: 'New contact'
    },
    {
        path: 'register',
        component: ContentHostComponent,
        data: {component: RegisterFormComponent},
        title: 'New contact'
    },
    {
        path: 'login',
        component: ContentHostComponent,
        data: {component: LoginFormComponent},
        title: 'New contact'
    },
    {
        path: 'error',
        component: ContentHostComponent,
        data: {component: ErrorComponent},
        title: 'New contact'
    }
];
