import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IForgotThePasswordComponent } from './i-forgot-the-password.component';

describe('IForgotThePasswordComponent', () => {
  let component: IForgotThePasswordComponent;
  let fixture: ComponentFixture<IForgotThePasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IForgotThePasswordComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IForgotThePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
