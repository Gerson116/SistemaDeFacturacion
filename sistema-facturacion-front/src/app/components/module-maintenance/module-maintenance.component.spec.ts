import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleMaintenanceComponent } from './module-maintenance.component';

describe('ModuleMaintenanceComponent', () => {
  let component: ModuleMaintenanceComponent;
  let fixture: ComponentFixture<ModuleMaintenanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuleMaintenanceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModuleMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
