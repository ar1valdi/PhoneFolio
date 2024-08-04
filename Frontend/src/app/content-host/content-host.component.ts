import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-content-host',
  standalone: true,
  imports: [],
  templateUrl: './content-host.component.html',
  styleUrl: './content-host.component.css'
})
export class ContentHostComponent {
  @ViewChild('formContainer', { read: ViewContainerRef, static: true }) formContainer!: ViewContainerRef;
  component: any;

  constructor(
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.component = data['component'];
      this.loadComponent();
    })
  }

  loadComponent(): void {
    this.formContainer.clear();
    this.formContainer.createComponent(this.component);
  }
}
