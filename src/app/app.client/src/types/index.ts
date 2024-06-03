interface AgePlusTwentyDto {
    userId: string;
    originalAge: number;
    agePlusTwenty: number;
}

interface TopColoursDto {
    colour?: string;
    count: number;
}

interface UserEntity {
    id: string;
    firstName?: string;
    lastName?: string;
    email?: string;
    dob?: string;
    favouriteColour?: string;
}

interface ResponseDto {
    users?: UserEntity[];
    ages?: AgePlusTwentyDto[];
    topColours?: TopColoursDto[];
}