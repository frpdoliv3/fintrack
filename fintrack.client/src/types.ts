import { Result } from "@utils/result"

export interface User {
    email: string
    userName: string
}

export interface UserContextState {
    user: Result<User, Error> | null
    updateUser: () => void
}

export interface AvatarProps {
    email: string
    name: string
    avatarPath?: string
}
