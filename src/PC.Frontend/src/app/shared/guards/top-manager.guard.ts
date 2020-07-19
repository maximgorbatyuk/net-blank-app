import { Injectable } from '@angular/core';
import { UserRole } from '@models/enums';
import { GuardBase } from './guard.base';

@Injectable()
export class TopManagerGuard extends GuardBase {
  protected roleToCheck(): UserRole {
    return UserRole.TopManager;
  }
}
